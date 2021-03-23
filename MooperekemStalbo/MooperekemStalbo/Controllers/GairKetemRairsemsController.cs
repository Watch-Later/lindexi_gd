﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using MooperekemStalbo;
using MooperekemStalbo.Models;

namespace MooperekemStalbo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GairKetemRairsemsController : ControllerBase
    {
        private readonly MooperekemStalboContext _context;
        private IHostingEnvironment _host;

        public GairKetemRairsemsController(MooperekemStalboContext context, IHostingEnvironment host)
        {
            _context = context;

            _host = host;

            Folder = Path.Combine(_host.WebRootPath, "Package");

            if (!_context.GairKetemRairsem.Any())
            {
                _context.SaveChanges();
            }
        }

        private string Folder { get; }

        // GET: api/GairKetemRairsems
        [HttpGet]
        public IEnumerable<GairKetemRairsem> GetGairKetemRairsem()
        {
            return _context.GairKetemRairsem;
        }

        // GET: api/GairKetemRairsems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGairKetemRairsem([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gairKetemRairsem = await _context.GairKetemRairsem.FindAsync(id);

            if (gairKetemRairsem == null)
            {
                return NotFound();
            }

            return Ok(gairKetemRairsem);
        }

        // PUT: api/GairKetemRairsems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGairKetemRairsem([FromRoute] long id,
            [FromBody] GairKetemRairsem gairKetemRairsem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gairKetemRairsem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gairKetemRairsem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GairKetemRairsemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GairKetemRairsems
        [HttpPost]
        public async Task<IActionResult> PostGairKetemRairsem([FromBody] GairKetemRairsem gairKetemRairsem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GairKetemRairsem.Add(gairKetemRairsem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGairKetemRairsem", new { id = gairKetemRairsem.Id }, gairKetemRairsem);
        }

        [HttpPost("UploadPackage")]
        public async Task<StatusCodeResult> UploadPackage([FromForm] KanajeaLolowge file)
        {
            var fileInfo = new FileInfo(Path.GetTempFileName());

            var fileStream = fileInfo.Open(FileMode.Create, FileAccess.ReadWrite);

            string fileSha;
            using (fileStream)
            {
                await file.File.CopyToAsync(fileStream);

                fileStream.Seek(0, SeekOrigin.Begin);

                fileSha = Shafile.GetFile(fileStream);
            }

            if (fileSha == file.Sha)
            {
                var medaltraFairjousuFowluNererisMoubeturce =
                    new MedaltraFairjousuFowluNererisMoubeturce(fileInfo, Folder, fileSha);

                if (medaltraFairjousuFowluNererisMoubeturce.CheckFile())
                {
                    var package = medaltraFairjousuFowluNererisMoubeturce.Package;

                    // 判断没有存在重复
                    if (_context.GairKetemRairsem.Any(temp =>
                        temp.Name == package.Name && temp.Version == package.Version))
                    {
                        return BadRequest();
                    }

                    var maytrawherehijooBoujallcheabel = new MaytrawherehijooBoujallcheabel()
                    {
                        File = "/storage/package/" +
                               medaltraFairjousuFowluNererisMoubeturce.MoveFile().Replace("\\", "/"),
                        Sha = fileSha
                    };

                    _context.MaytrawherehijooBoujallcheabel.Add(maytrawherehijooBoujallcheabel);

                    var gairKetemRairsem = new GairKetemRairsem
                    {
                        Name = package.Name,
                        Version = package.Version,
                        RequirementMaxVersion = package.RequirementMaxVersion,
                        RequirementMinVersion = package.RequirementMinVersion,
                        File = maytrawherehijooBoujallcheabel.Id
                    };


                    _context.GairKetemRairsem.Add(gairKetemRairsem);

                    _context.SaveChanges();
                }


                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Download")]
        public ActionResult Download([FromBody] KebunerNeefunadrow saljudecooBolor)
        {
            Console.WriteLine("Download");
            var version = new Version(saljudecooBolor.Version);
            var gairKetemRairsem = _context.GairKetemRairsem
                .Where(temp => temp.Name == saljudecooBolor.Name
                               && new Version(temp.RequirementMinVersion) <= version
                               && new Version(temp.RequirementMaxVersion) > version)
                .OrderBy(temp => new Version(temp.Version)).FirstOrDefault();
            if (gairKetemRairsem != null)
            {
                var maytrawherehijooBoujallcheabel =
                    _context.MaytrawherehijooBoujallcheabel.FirstOrDefault(temp => temp.Id == gairKetemRairsem.File);

                if (maytrawherehijooBoujallcheabel != null)
                {
                    return new JsonResult(new GemurboostatelnearseRurallnawrear
                    {
                        File = maytrawherehijooBoujallcheabel.File,
                        Sha = maytrawherehijooBoujallcheabel.Sha,
                        Version = gairKetemRairsem.Version
                    });
                }
            }

            Console.WriteLine("找不到文件");

            return Ok();
        }

        private bool GairKetemRairsemExists(long id)
        {
            return _context.GairKetemRairsem.Any(e => e.Id == id);
        }
    }
}