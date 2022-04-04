using PopCorn.Web.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Domain
{
    [Table("Sessions")]
    public class Session
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public double TicketValue { get; set; }
        public ETypeAnimation TypeAnimation { get; set; }
        public ETypeAudio TypeAudio { get; set; }
        public int MovieId { get; set; }        
        public int RoomId { get; set; }     

        public void CaculatorEndTime(string duration)
        {
            var dtStartMovie = SessionDate.Date.AddHours(double.Parse(StartTime.Split(':')[0])).AddMinutes(double.Parse(StartTime.Split(':')[1]));
            var dtEndMovie = dtStartMovie.AddHours(double.Parse(duration.Split(':')[0])).AddMinutes(double.Parse(duration.Split(':')[1]));
            string minutes = dtEndMovie.Minute > 10 ? dtEndMovie.Minute.ToString() : $"0{dtEndMovie.Minute}";
            EndTime = $"{dtEndMovie.Hour}:{minutes}";
            StartTime = $"{dtStartMovie.Hour}:{minutes}";
        }

        public bool SalaOcupada(DateTime dateInicial,DateTime dateFinal)
        {
            var dataInicial = SessionDate.AddHours(double.Parse(StartTime.Split(':')[0])).AddMinutes(double.Parse(StartTime.Split(':')[1]));
            var dataFim = SessionDate.AddHours(double.Parse(EndTime.Split(':')[0])).AddMinutes(double.Parse(EndTime.Split(':')[1]));

            return (dateInicial >= dataInicial && dateInicial <= dataFim)
                      ||(dateFinal >= dataInicial && dateFinal <= dataFim);
        }

    }
}
