using Mirai_CSharp.Models;

namespace KQ.Model
{
    class BaseInfo : IBaseInfo
    {
        public override string ToString()
        {
            return $"{Name}({Id})";
        }

        public BaseInfo(IBaseInfo baseInfo)
        {
            Id = baseInfo.Id;
            Name = baseInfo.Name;
        }

        public BaseInfo(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}