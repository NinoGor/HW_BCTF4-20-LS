namespace Lecture10_HW.Davaleba1
{
    internal class Cello : MusicalInstrument
    {
        public Cello()
            : base(
                "Cello",
                "A large bowed string instrument.",
                "The cello was developed in Europe during the 16th century.")
        {}

        protected override string[] GetFamousPieces()
        {
            return
            [
                "Cello Suite No.1",
                "The Swan",
                "Cello Concerto in B Minor",
                "Kol Nidrei"
            ];
        }

        public override void Sound()
        {
            Console.WriteLine($" * You hear the deep sound of a cello playing \"{GetRandomPiece()}\". *");
        }
    }
}
