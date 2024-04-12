namespace Formazione.Models
{
    public class CustomViewProgEdiz
    {

        public IEnumerable<ProgettoFormativo>? listaProgetti { get; set; }
        public IEnumerable<EdizioneCorso>? listaEdizioni { get; set; }

        public IEnumerable<Modulo>? listaModuli { get; set; }

        public int CalcolaEdizioniCorso(int? prmTot,int prmMaxDisc)
        {
            int iResult = 0;
            if(prmTot!=null)
                iResult=(int)prmTot/prmMaxDisc;
            return iResult;

        }

        public int CalcolaEdizioniCorsoMancanti(int? prmTot, int? prmTotEdizioniCreate, int prmMaxDisc)
        {
            int iResult = 0;
            if (prmTot!=null)
            {
                iResult=(int)prmTot/prmMaxDisc;
                iResult=iResult-(int)prmTotEdizioniCreate;
            }
            return iResult;

        }

    }
}
