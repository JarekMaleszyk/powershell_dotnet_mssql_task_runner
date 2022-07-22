using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using offler_backend_api.Exceptions;
using offler_db_context.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Services
{
    public interface IAttachmentFileService
    {
        bool UploadFileByCode(IFormFile file, string code);
    }
    public class AttachmentFileService : IAttachmentFileService
    {
        private readonly ILogger<AttachmentFileService> _logger;
        private readonly OfflerDbContext _context;
        private readonly IConfiguration _configuration;
        public AttachmentFileService(OfflerDbContext context
            , ILogger<AttachmentFileService> logger
            , IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        public bool UploadFileByCode(IFormFile file, string code)
        {
            var dctPath = _configuration.GetValue<string>("Other:DctFolder");
            var toSendPath = _configuration.GetValue<string>("Other:OfferToSendFolder");
            var receivedPath = _configuration.GetValue<string>("Other:OfferReceivedFolder");

            _logger.LogTrace($"Dictionary path: {dctPath}");
            _logger.LogTrace($"Ready to send path: {toSendPath}");
            _logger.LogTrace($"Received offer path: {receivedPath}");

            if (file.Length <= 0)
                throw new BadRequestException($"File upload error.");

            Directory.CreateDirectory(toSendPath);
            foreach (var subDir in new DirectoryInfo(toSendPath).GetDirectories())
            {
                subDir.Delete(true);
            }

            var filePath = Path.Combine(toSendPath, file.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);

            try
            {
                file.CopyTo(stream);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"File upload error: {ex.Message}");
            }
        }
    }
}
