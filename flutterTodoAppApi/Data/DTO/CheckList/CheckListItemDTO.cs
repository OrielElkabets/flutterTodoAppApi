namespace flutterTodoAppApi.Data.DTO.CheckList
{
    public class CheckListItemDTO
    {
        private int? _id = null;
        public int? Id
        {
            get
            {
                return _id ?? 0;
            }
            set
            {
                _id = value;
            }
        }
        public required string Text { get; set; }
        public required bool Checked { get; set; }
    }
}
