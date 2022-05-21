using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			if (visits.Count == 0)
            {
				return 0;
			}

			var rslt = visits
				.GroupBy(visit => visit.UserId)
				.Select(group => group.OrderBy(visitRecord => visitRecord.DateTime))
				.Select(visitRecords => visitRecords.Bigrams())
				.SelectMany(k => k)
				.Where(pair => pair.Item1.SlideType == slideType && pair.Item1.SlideId != pair.Item2.SlideId)
				.Select(pair => (pair.Item2.DateTime - pair.Item1.DateTime).TotalMinutes)
				.Where(minT => minT >= 1 && minT <= 120);

			var count = rslt.Count();

			if (count == 0)
            {
				return 0;
			}
			return rslt.Median();
		}
	}
}