using Mirai_CSharp.Models;


namespace KQ.Model
{
    class UnitInfo
    {
        public string Name { get;  }
        public long Id { get; }
        public UnitInfo(IBaseInfo info)
        {
            Name = info.Name;
            Id = info.Id;
        }

        public override string ToString()
        {
            return $"{Name}({Id})";
        }
    }
}
