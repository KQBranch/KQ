using Mirai_CSharp.Models;

namespace KQ.Model
{
    class BaseInfo : IBaseInfo
    {
        private IBaseInfo _baseInfo;

        public override string ToString()
        {
            return $"{_baseInfo.Name}({_baseInfo.Id})";
        }

        public BaseInfo(IBaseInfo baseInfo)
        {
            _baseInfo = baseInfo;
        }

        public long Id
        {
            get => _baseInfo.Id;
        }

        public string Name
        {
            get => _baseInfo.Name;
        }
    }
}