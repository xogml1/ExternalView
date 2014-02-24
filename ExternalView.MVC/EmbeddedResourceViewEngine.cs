using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace ExternalView.Mvc
{
    public class EmbeddedResourceViewEngine : RazorViewEngine
    {
        // cshtml만 취급!!
        private static readonly string FileExtension = ".cshtml";

        // Copy해 올 View 파일의 Target Path
        private static readonly string ERVViewPath = "~/Views/EmbeddedResourceView/";

        // 하위 폴더 생성을 위한 prefix
        private static readonly string areasView = ".Areas.";

        public EmbeddedResourceViewEngine(Type embededResourceAssemblyType)
        {
            // ERV 는 Areas 만 사용함
            AreaViewLocationFormats = new string[] {
                "~/Views/EmbeddedResourceView/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Views/EmbeddedResourceView/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = AreaPartialViewLocationFormats = AreaViewLocationFormats;

            // 사용하지 않음 ( 설정하지 않을경우 Default 설정으로 인해 Routing 오작동 )
            ViewLocationFormats = new string [] {
                "~/UNUSED"
            };

            MasterLocationFormats = PartialViewLocationFormats = ViewLocationFormats;
            
            DumpOutViews(embededResourceAssemblyType);
        }

        private static void DumpOutViews(Type embededResourceAssemblyType)
        {
            IEnumerable<string> resources = embededResourceAssemblyType.Assembly.GetManifestResourceNames().Where(name => name.EndsWith(FileExtension));
            foreach (string res in resources) { DumpOutView(res, embededResourceAssemblyType); }
        }

        private static void DumpOutView(string res, Type embededResourceAssemblyType)
        {
            string rootPath = HttpContext.Current.Server.MapPath(ERVViewPath);

            //포함된 리소스 파일을 지정 위치로 copy
            Stream resStream = embededResourceAssemblyType.Assembly.GetManifestResourceStream(res);

            // Extension 제거 - 파일 생성시 추가
            string filePath = res.Replace(FileExtension, "");
            int lastSeparatorIdx = filePath.LastIndexOf(".");
            string fileName = filePath.Substring(lastSeparatorIdx + 1);
            filePath = filePath.Substring(0, lastSeparatorIdx + 1);

            // 불필요한 네임스페이스 제거
            // Area 하위 View 인 경우 /Areas/ 하위로 생성
            if (filePath.IndexOf(areasView) >= 0)
            {
                int indexOfView = filePath.IndexOf(areasView);
                filePath = filePath.Substring(indexOfView + 1);

                // 네임스페이스의 . 을 폴더 구조 / 으로 치환
                filePath = rootPath + filePath.Replace(".", "/");

                // 폴더가 없을 경우 폴더 생성
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                SaveStreamToFile(filePath + fileName + FileExtension, resStream);
            }
        }

        private static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }
    }
}
