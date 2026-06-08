namespace Lecture10_HW.Davaleba1
{
    internal class Ukulele : MusicalInstrument
    {
        public Ukulele()
            : base(
                "Ukulele",
                "A small four-stringed instrument.",
                "The ukulele originated in Hawaii in the 19th century.")
        {}

        protected override string[] GetFamousPieces()
        {
            return
            [
                "Somewhere Over the Rainbow",
                "Riptide",
                "Blue Hawaii",
                "Tiny Bubbles"
            ];
        }

        public override void Sound()
        {
            Console.WriteLine($" * You hear the strumming of \"{GetRandomPiece()}\" on an ukulele. *");
        }
    }
}
