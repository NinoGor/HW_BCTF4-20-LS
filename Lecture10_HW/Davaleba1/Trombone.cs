namespace Lecture10_HW.Davaleba1
{
    internal class Trombone : MusicalInstrument
    {
        public Trombone()
            : base(
                "Trombone",
                "A brass instrument with a slide.",
                "The trombone appeared during the Renaissance period.")
        { }

        protected override string[] GetFamousPieces()
        {
            return
            [
                "Blue Bells of Scotland",
                "Trombone Concerto",
                "Bolero",
                "76 Trombones"
            ];
        }

        public override void Sound()
        {
            Console.WriteLine($" * A trombone powerfully blasts \"{GetRandomPiece()}\". *");
        }
    }
}
