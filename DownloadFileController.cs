                public async Task<IActionResult> Download(){
               return new PushStreamResult(
                    async (outputStream) =>
                    {
                        using (var zip = new ZipArchive(new WriteOnlyStreamWrapper(outputStream),
                            ZipArchiveMode.Create))
                        {
                            foreach (var f in sourceFiles)
                            {
                                var filePathTemp = "path from you will read the file";
                                using (var res = System.IO.File.OpenRead(filePathTemp))
                                {
                                    var entry = zip.CreateEntry(FileHelpers.GetFileName(f.Title, f.Extension));
                                    using (var entryStream = entry.Open())
                                    {
                                        await res.CopyToAsync(entryStream);
                                    }
                                    res.Close();
                                }
                            }
                        }
                    }, "application/octet-stream", "file_download_name.zip");
                    }
