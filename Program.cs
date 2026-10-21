using System;
using System.Collections.Generic;

namespace KerangkaGame
{
    enum ModePindah
    {
        Console = 1,
        Keyboard = 2
    }

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
        
        public void PindahPosisi(ModePindah mode)
        {
            Console.WriteLine($"posisi saat ini : {posisiSekarang}");

            if (mode == ModePindah.Console)
            {
                Console.WriteLine("Mode Console: masukkan angka untuk mengubah posisi.");
                Console.WriteLine("Angka positif menambah, angka negatif mengurangi.");
                Console.WriteLine("Ketik 0 atau kosongkan untuk selesai.");

                while (true)
                {
                    Console.Write("Masukkan perpindahan angka: ");
                    string input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine($"Pindah selesai, posisi akhir : {posisiSekarang}");
                        break;
                    }

                    if (int.TryParse(input, out int perpindahan))
                    {
                        if (perpindahan == 0)
                        {
                            Console.WriteLine($"Pindah selesai, posisi akhir : {posisiSekarang}");
                            break;
                        }

                        posisiSekarang += perpindahan;
                        Console.WriteLine($"Angka {perpindahan} diterima, posisi saat ini : {posisiSekarang}");
                    }
                    else
                    {
                        Console.WriteLine("Input tidak valid, masukkan angka bulat saja.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Mode Keyboard:");
                Console.WriteLine("Tekan Arrow KIRI untuk menambah");
                Console.WriteLine("Tekan Arrow KANAN untuk mengurangi");
                Console.WriteLine("Tekan Esc untuk selesai.");

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
        }

        public void getData()
        {
            Console.WriteLine($"Karakter {nama} | Posisi Awal: {posisiAwal} | Posisi Saat Ini: {posisiSekarang}");
        }
    }

    class MainProgram
    {
        static ModePindah PilihMode()
        {
            while (true)
            {
                Console.WriteLine("Pilih mode perpindahan posisi:");
                Console.WriteLine("1. Mode Console (perpindahan angka)");
                Console.WriteLine("2. Mode Keyboard (Arrow Kiri/Kanan, Esc untuk selesai)");
                Console.Write("Masukkan pilihan (1 atau 2): ");

                string input = Console.ReadLine()?.Trim();

                if (input == "1")
                {
                    Console.WriteLine("Anda memilih Mode Console.");
                    return ModePindah.Console;
                }
                if (input == "2")
                {
                    Console.WriteLine("Anda memilih Mode Keyboard.");
                    return ModePindah.Keyboard;
                }

                Console.WriteLine("Pilihan tidak valid, silakan pilih 1 atau 2.");
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Karakter player1 = new Karakter("Asep", 100, 10, 0); 
            Karakter musuh = new Karakter("Bahlil", 100, 100, 10); 

            player1.Serang(musuh);
            player1.getData();

            Console.WriteLine();

            ModePindah mode = PilihMode();
            Console.WriteLine();
            player1.PindahPosisi(mode);

            Console.WriteLine();
            musuh.Heal();
        }
    }
}