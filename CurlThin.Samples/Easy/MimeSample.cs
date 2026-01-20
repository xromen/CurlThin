using CurlThin.Enums;
using CurlThin.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static CurlThin.CurlNative;

namespace CurlThin.Samples.Easy
{
    internal class MimeSample : ISample
    {
        public void Run()
        {
            // curl_global_init() with default flags.
            var global = CurlNative.Init();

            // curl_easy_init() to create easy handle.
            var easy = CurlNative.Easy.Init();
            var mime = Mime.Init(easy);
            try
            {
                var dataCopier = new DataCallbackCopier();

                var mimePart = Mime.AddPart(mime);
                Mime.Name(mimePart, "subject");
                Mime.Data(mimePart, "TestSubject", CurlConstants.ZERO_TERMINATED);

                var mimeFilePart = Mime.AddPart(mime);
                Mime.Name(mimePart, "subject");
                Mime.FileData(mimePart, "/path_to_file");

                CurlNative.Easy.SetOpt(easy, CURLoption.MIMEPOST, mime.DangerousGetHandle());
                CurlNative.Easy.SetOpt(easy, CURLoption.URL, "http://httpbin.org/ip");
                CurlNative.Easy.SetOpt(easy, CURLoption.WRITEFUNCTION, dataCopier.DataHandler);

                var result = CurlNative.Easy.Perform(easy);

                Console.WriteLine($"Result code: {result}.");
                Console.WriteLine();
                Console.WriteLine("Response body:");
                Console.WriteLine(Encoding.UTF8.GetString(dataCopier.Stream.ToArray()));
            }
            finally
            {
                easy.Dispose();

                if (global == CURLcode.OK)
                {
                    CurlNative.Cleanup();
                }
            }
        }
    }
}
