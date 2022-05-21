using System;
using System.Text;

namespace hashes
{
    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
        IMagic
    {
        private readonly byte[] documentArray = { 8, 4, 4, 8 };
        private static Vector vector = new Vector(0, 0);
        Document document;
        Segment segment = new Segment(vector, vector);
        Cat cat = new Cat("Tom", "American", DateTime.MaxValue);
        Robot robot = new Robot("BostomDynamicsRobot");

        public GhostsTask() => document = new Document("C#", Encoding.UTF8, documentArray);

        public void DoMagic()
        {
            vector.Add(new Vector(1, 1));
            segment.End.Add(new Vector(2, 2));
            cat.Rename("Cat");
            documentArray[0] = 1;
            Robot.BatteryCapacity *= 100;
        }

        Document IFactory<Document>.Create()
        {
            return document;
        }

        Vector IFactory<Vector>.Create()
        {
            return vector;
        }

        Segment IFactory<Segment>.Create()
        {
            return segment;
        }

        Cat IFactory<Cat>.Create()
        {
            return cat;
        }

        Robot IFactory<Robot>.Create()
        {
            return robot;
        }
    }
}