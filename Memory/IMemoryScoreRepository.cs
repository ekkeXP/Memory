using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    //Interface for database connectivity
    public interface IMemoryScoreRepository
    {
        int Insert(MemoryHighscore newToDoItem);
        List<MemoryHighscore> GetAll();
        int GetRank(int score);
        String[] GetImages();
        void UploadImage(string Img);
        void DeleteImg(string url);
    }
}
