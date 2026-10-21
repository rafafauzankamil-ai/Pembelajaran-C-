using System;
using System.Collections.Generic;

namespace KerangkaGame
{
    class Karakter
    {
        public string nama { get; private set; }
        public int kesehatan { get; private set; }
        public int senjata { get; private set; }

        public int posisiAwal { get; private set; }
        public int posisiSekarang { get; private set; }

        public Karakter(string nama, int kesehatan, int senjata, int posisiAwal = 0)
        {
            this.nama = nama;
            this.kesehatan = kesehatan;
            this.senjata = senjata;
            this.posisiAwal = posisiAwal;
            this.posisiSekarang = posisiAwal;
        }

        public void Serang(Karakter target)
        {
            Console.WriteLine("===> Mulai Serangan");
            target.TerimaSerangan(this.senjata);
        }

        public void TerimaSerangan(int jumlahSerangan)
        {
            kesehatan -= jumlahSerangan;
            Console.WriteLine($"{nama} diserang dengan {jumlahSerangan}. Sisa kesehatan {kesehatan}");
        }

        public void Heal()
        {
            Console.Write($"Apakah {nama} ingin melakukan healing? (y/n): ");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y")
            {
                int jumlahHealing = 20;
                kesehatan += jumlahHealing;
                Console.WriteLine($"{nama} berhasil healing (+{jumlahHealing} HP). Sisa kesehatan {kesehatan}");
            }
            else if (input == "n")
            {
                Console.WriteLine($"{nama} batal melakukan healing.");
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid / tidak ada input yang jelas.");
            }
        }
        
        public void PindahPosisi()
        {
            Console.WriteLine($"posisi saat ini : {posisiSekarang}");
            Console.WriteLine("Tekan Arrow KIRI untuk menambah");
            Console.WriteLine("Tekan Arrow KANAN untuk mengurangi");
            Console.WriteLine("Tekan Enter atau Esc untuk selesai.");

            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.LeftArrow)
                {
                    posisiSekarang += 1;
                    Console.WriteLine($"Arrow Kiri ditekan, posisi saat ini : {posisiSekarang}");
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    posisiSekarang -= 1;
                    Console.WriteLine($"Arrow Kanan ditekan, posisi saat ini : {posisiSekarang}");
                }
                else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine($"Pindah selesai, posisi akhir : {posisiSekarang}");
                    break;
                }
            }
        }

        public void getData()
        {
            Console.WriteLine($"Karakter {nama} | Posisi Awal: {posisiAwal} | Posisi Saat Ini: {posisiSekarang}");
        }
    }

    class MainProgram
    {
        static void Main(string[] args)
        {
            Karakter player1 = new Karakter("Asep", 100, 10, 0); 
            Karakter musuh = new Karakter("Bahlil", 100, 100, 10); 

            player1.Serang(musuh);
            player1.getData();

            Console.WriteLine();
            
            player1.PindahPosisi();

            Console.WriteLine();
            musuh.Heal();
        }
    }
}