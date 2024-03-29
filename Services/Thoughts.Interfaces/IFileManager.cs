﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Thoughts.DAL.Entities;

namespace Thoughts.Interfaces;

public interface IFileManager
{

    Task<UploadedFile?> Get(string sha1);

    Task<string> AddOrUpdate(UploadedFile fileModel);

    Task Delete(string sha1);

    Task<bool> SoftDelete(string sha1);

    Task<bool> ActivateFile(string sha1);

    Task<bool> Exists(string sha1);

    Task<bool> Exists(byte[] buffer);

    Task<List<UploadedFile>> GetAllFilesInfo();
}
