using Application.DTO.Common;
using Application.DTO.LauncherDTO.LNC_004;
using Application.DTO.LauncherDTO.LNC_008;
using Application.DTO.LauncherDTO.LNC_009;
using Application.DTO.LauncherDTO.LNC_010;
using Application.DTO.LauncherDTO.LNC_011;
using Application.DTO.LauncherDTO.LNC_012;
using Application.DTO.LauncherDTO.LNC_013;
using Application.MessageHandlers.ReleaseRequestHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Diagnostics;
using System.IO.Compression;

namespace UpdaterServer.Controllers;

[Route("api/[controller]")]
public class ReleasesController : BaseApiController
{
	[HttpGet("downloadRelease/{id}")]
	public async Task<IActionResult> DownloadRelease(Guid Id)
	{
		var response = await Mediator.Send(new LNC_008(Id));
		if (response == null)
			return NotFound();
		var release = ((LNC_009)response).Release;

		if (!System.IO.File.Exists(release.Path))
			return BadRequest("File not found!");

		var provider = new FileExtensionContentTypeProvider();
		string contentType;
		if (!provider.TryGetContentType(release.Path, out contentType))
		{
			contentType ??= "application/octet-stream";
		}

		FileStream fileStream = new FileStream(release.Path, FileMode.Open, FileAccess.Read);
		return File(fileStream, contentType);
	}

	[HttpPost("addRelease/{id}")]
	[RequestFormLimits(MultipartBodyLengthLimit = long.MaxValue)]
	[RequestSizeLimit(long.MaxValue)]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IActionResult> CreateNewReleaseAsync(Guid Id, [FromForm] IFormFile file)
	{
		var response = await Mediator.Send(new LNC_010(Id));
		if (response == null)
			return NotFound($"Application with Id = {Id} not found");
		var project = ((LNC_004)response).Project;
		string appFolder = Path.Combine(ReleasesPath, project.Name);

		if (!Directory.Exists(appFolder))
			Directory.CreateDirectory(appFolder);

		await using var requestFileStream = file.OpenReadStream();
		using var zipArchive = new ZipArchive(requestFileStream);
		var exefile = zipArchive.Entries.Where(x => x.Name.Contains(project.ExeFile)).FirstOrDefault();

		if (exefile == null)
			return NotFound($"Exe file {project.ExeFile} not found!");

		var tempFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		var tempExeFile = Path.Combine(tempFolder, Path.GetFileName(exefile.Name));
		if (!Directory.Exists(tempFolder))
			Directory.CreateDirectory(tempFolder);
		exefile.ExtractToFile(tempExeFile);
		var fileVersion = FileVersionInfo.GetVersionInfo(tempExeFile).FileVersion;
		Directory.Delete(tempFolder, true);
		string filePath = Path.Combine(appFolder, $"{fileVersion}-{project.Name}{Path.GetExtension(file.FileName)}");
		await using var fs = System.IO.File.Create(filePath);

		await file.CopyToAsync(fs);
		await fs.FlushAsync();
		var release = await Mediator.Send(new CreateReleaseAssemblyRequest(Id, filePath, fileVersion));
		return Ok(release);
	}

	[HttpGet("downloadLastRelease/{id}")]
	public async Task<IActionResult> DownloadLastRelease(Guid Id)
	{
		var response = await Mediator.Send(new LNC_011(Id));
		if (response == null)
			return NotFound();
		var release = ((LNC_009)response).Release;
		if (!System.IO.File.Exists(release.Path))
			return BadRequest();

		var provider = new FileExtensionContentTypeProvider();
		string contentType;
		if (!provider.TryGetContentType(release.Path, out contentType))
		{
			contentType ??= "application/octet-stream";
		}

		FileStream fileStream = new FileStream(release.Path, FileMode.Open, FileAccess.Read);
		return File(fileStream, contentType);
	}

	[HttpPost("patchNote")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IResponse> CreatePatchNoteAsync(LNC_012 request) => await Mediator.Send(request);

	[HttpDelete("patchNote/{id:guid}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Dev")]
	public async Task<IResponse> DeletePatchNoteAsync(Guid id) => await Mediator.Send(new LNC_013(id));
}
