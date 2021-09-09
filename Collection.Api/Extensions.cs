namespace Collection {
    public static class Extensions {
        public static Dtos.Item asDto(this Entities.Item item) {
            return new Dtos.Item {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}