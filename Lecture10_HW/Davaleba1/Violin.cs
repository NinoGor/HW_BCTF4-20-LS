namespace Lecture10_HW.Davaleba1
{
    internal class Violin : MusicalInstrument
    {
        public Violin()
            : base(
                "Violin",
                "A bowed string instrument with four strings.",
                "The modern violin was developed in Italy during the 16th century.")
        {}

        protected override string[] GetFamousPieces()
        {
            return
            [
                "The Four Seasons",
                "Violin Concerto in D Major",
                "Zigeunerweisen",
                "Meditation from Thais"
            ];
        }

        public override void Sound()
        {
            Console.WriteLine($" * You hear the graceful performance of \"{GetRandomPiece()}\" on violin. *");
        }
    }
}
