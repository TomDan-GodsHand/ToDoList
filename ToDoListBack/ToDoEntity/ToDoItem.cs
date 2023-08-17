using Coldairarrow.Util;

namespace ToDoEntity
{
    public enum SortEnum
    {
        CreateDate,
        Important,
        DeadLine,
        FristCode
    }

    public class ToDoItem
    {
        private string id;
        private string name;
        private DateTime createDate;
        private DateTime alertTime;
        private DateTime deadLine;
        private DateTimeOffset repeatTime;
        private List<ToDoItemStep> step;
        private bool completed;
        private bool important;
        private string remark;
        private DateTime completedDate;
        private ToDoItemType type;

        public string Id
        {
            get
            {
                if (id == null)
                {
                    id = IdHelper.GetId();
                }
                return id;
            }
            set => id = value;
        }

        /// <summary>
        ///     标题
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get => remark; set => remark = value; }

        /// <summary>
        ///     重要事项标记
        /// </summary>
        public bool Important { get => important; set => important = value; }

        /// <summary>
        ///     完成标记
        /// </summary>
        public bool Completed { get => completed; set => completed = value; }

        /// <summary>
        ///     完成时间
        /// </summary>
        public DateTime CompletedDate { get => completedDate; set => completedDate = value; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get => createDate; set => createDate = value; }

        /// <summary>
        ///     提醒日期
        /// </summary>
        public DateTime AlertTime { get => alertTime; set => alertTime = value; }

        public DateTime DeadLine { get => deadLine; set => deadLine = value; }

        /// <summary>
        ///     重复间隔
        /// </summary>
        public DateTimeOffset RepeatTime { get => repeatTime; set => repeatTime = value; }

        public List<ToDoItemStep> Step { get => step; set => step = value; }
        public ToDoItemType Type { get => type; set => type = value; }

        public bool SetCompleted()
        {
            try
            {
                completed = true;
                completedDate = DateTime.Now;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}