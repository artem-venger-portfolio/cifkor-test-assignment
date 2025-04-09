namespace WebClient
{
    public readonly struct DogBreedDescription
    {
        public DogBreedDescription(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}