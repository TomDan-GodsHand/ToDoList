namespace ToDoEntity
{
    public class ToDoTaskItem
    {
        private int id;
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

        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get => remark; set => remark = value; }

        /// <summary>
        /// 重要事项标记
        /// </summary>
        public bool Important { get => important; set => important = value; }

        /// <summary>
        /// 完成标记
        /// </summary>
        public bool Completed { get => completed; set => completed = value; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompletedDate { get => completedDate; set => completedDate = value; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get => createDate; set => createDate = value; }

        /// <summary>
        /// 提醒日期
        /// </summary>
        public DateTime AlertTime { get => alertTime; set => alertTime = value; }

        /// <summary>
        /// 截至日期
        /// </summary>
        public DateTime DeadLine { get => deadLine; set => deadLine = value; }

        /// <summary>
        /// 重复间隔
        /// </summary>
        public DateTimeOffset RepeatTime { get => repeatTime; set => repeatTime = value; }

        public List<ToDoItemStep> Step { get => step; set => step = value; }
    }
}