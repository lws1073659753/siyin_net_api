namespace SiyinPractice.Shared.Core.Exceptions
{
    public class SiyinPracticeMessage
    {
        protected SiyinPracticeMessage(string resourceName, int paramsCount = 0)
        {
            ResourceName = resourceName;
            ParamsCount = paramsCount;
        }

        public int ParamsCount { get; private set; }
        public string ResourceName { get; private set; }

        public static readonly SiyinPracticeMessage DTO_IS_NULL = new SiyinPracticeMessage("传入数据不能为null", 0);

        public static readonly SiyinPracticeMessage DELETE_IS_NULL = new SiyinPracticeMessage("删除数据不能为null.", 0);

        public static readonly SiyinPracticeMessage DELETE_NONEXIST = new SiyinPracticeMessage("删除数据不存在.", 0);

        public static readonly SiyinPracticeMessage ENTITY_EXIST = new SiyinPracticeMessage("{0}已存在.", 1);

        public static readonly SiyinPracticeMessage NOT_DELETE = new SiyinPracticeMessage("数据不能删除.", 0);
    }
}