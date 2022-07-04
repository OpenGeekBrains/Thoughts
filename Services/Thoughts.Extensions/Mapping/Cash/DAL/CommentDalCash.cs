using CommentDal = Thoughts.DAL.Entities.Comment;
using Thoughts.Interfaces.Base.Mapping;

namespace Thoughts.Extensions.Mapping.Cash.DAL
{
    public class CommentDalCash : ICash<int, CommentDal>
    {
        public Dictionary<int, CommentDal> Cash { get; } = new();
    }
}
