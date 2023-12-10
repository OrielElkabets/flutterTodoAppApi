namespace flutterTodoAppApi.Data.DTO.CheckList
{
    public class CheckListDTO
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
        public required DateTime CreationDate { get; set; }
        public required DateTime LastModified { get; set; }
        public required string Title { get; set; }
        public required IEnumerable<CheckListItemDTO> Items { get; set; }
    }
}
