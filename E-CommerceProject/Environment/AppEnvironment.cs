using E_CommerceProject.Core.Interfaces;

namespace E_CommerceProject.Environment
{
    public class AppEnvironment : IAppEnvironment
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AppEnvironment(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string WebRootPath => _webHostEnvironment.WebRootPath;
    }
}