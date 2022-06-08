using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Thoughts.Domain.Base.Entities;
using Thoughts.Interfaces;
using Thoughts.Interfaces.Base.Repositories;

namespace Thoughts.Services.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly IRepository<DAL.Entities.Tag> _DALTagRepository;
        private readonly IMapper<Tag> _MapperDomain;
        private readonly IMapper<DAL.Entities.Tag> _MapperDAL;
        private readonly IMapper<Tag, DAL.Entities.Tag> _Mapper;

        public TagRepository(
            IRepository<DAL.Entities.Tag> DALTagRepository, 
            IMapper<Tag> MapperDomain,
            IMapper<DAL.Entities.Tag> MapperDAL,
            IMapper<Tag, DAL.Entities.Tag> Mapper)
        {
            _DALTagRepository = DALTagRepository;
            _MapperDomain = MapperDomain;
            _MapperDAL = MapperDAL;
            _Mapper = Mapper;
        }

        public async Task<bool> ExistId(int Id, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<bool> Exist(Tag item, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<int> GetCount(CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<IEnumerable<Tag>> GetAll(CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<IEnumerable<Tag>> Get(int Skip, int Count, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<IPage<Tag>> GetPage(int PageNumber, int PageSize, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<Tag?> GetById(int Id, CancellationToken Cancel = default)
        {
            var db_tag = await _DALTagRepository.GetById(Id, Cancel).ConfigureAwait(false);
            if (db_tag is null) return null;

            var domain_tag = _MapperDomain.Map(db_tag);
            return domain_tag;
        }

        public async Task<Tag> Add(Tag item, CancellationToken Cancel = default)
        {
            //var db_tag = _MapperDAL.Map(item);
            //var added_db_tag = await _DALTagRepository.Add(db_tag, Cancel).ConfigureAwait(false);
            //return _MapperDomain.Map(added_db_tag);

            var db_tag = _Mapper.Map(item);
            var added_db_tag = await _DALTagRepository.Add(db_tag, Cancel).ConfigureAwait(false);
            return _Mapper.Map(added_db_tag);
        }

        public async Task AddRange(IEnumerable<Tag> items, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<Tag> Update(Tag item, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task UpdateRange(IEnumerable<Tag> items, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<Tag> Delete(Tag item, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task DeleteRange(IEnumerable<Tag> items, CancellationToken Cancel = default) => throw new NotImplementedException();

        public async Task<Tag> DeleteById(int id, CancellationToken Cancel = default) => throw new NotImplementedException();
    }
}
