﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Template
{
    public class Day_2019_12
    {
        public static string firstPuzzle(string location)
        {
            List<Moon> moons = new List<Moon>();
            string[] lines = File.ReadAllLines(@location, Encoding.UTF8);
            foreach (var line in lines)
            {
                int x = 0;
                int y = 0;
                int z = 0;
                string[] splittedLine = line.Split(',');
                x = Int32.Parse(Regex.Match(splittedLine[0], @"-?\d+").Value);
                y = Int32.Parse(Regex.Match(splittedLine[1], @"-?\d+").Value);
                z = Int32.Parse(Regex.Match(splittedLine[2], @"-?\d+").Value);
                Moon m = new Moon(x, y, z);
                moons.Add(m);
            }
            for (int timeSteps = 0; timeSteps < 1000; timeSteps++)
            {
                //update velocity by appling gravity ()
                applyGravity(moons.ElementAt(0), moons.ElementAt(1));
                applyGravity(moons.ElementAt(0), moons.ElementAt(2));
                applyGravity(moons.ElementAt(0), moons.ElementAt(3));

                applyGravity(moons.ElementAt(1), moons.ElementAt(2));
                applyGravity(moons.ElementAt(1), moons.ElementAt(3));

                applyGravity(moons.ElementAt(2), moons.ElementAt(3));
                //update position
                foreach (var moon in moons)
                {
                    updatePosition(moon);
                }
                //next timestep
            }
            int totalEnergy = 0;
            //Console.WriteLine();
            foreach (var moon in moons)
                {
                    //Console.WriteLine("x:" + moon.x + " y:" + moon.y + " z:" + moon.z + " vx:" + moon.vx + " vy:" + moon.vy + " vz:" + moon.vz);
                    totalEnergy += moon.getPotentialEnergy() * moon.getKineticEnergy();
                }
            return totalEnergy.ToString();
        }

        public static string secondPuzzle(string location)
        {
            return "-";
        }
        public static void applyGravity(Moon a, Moon b)
        {
            if (a.x > b.x)
            {
                b.vx++;
                a.vx--;
            }
            else if (a.x < b.x)
            {
                b.vx--;
                a.vx++;
            }
            if (a.y > b.y)
            {
                b.vy++;
                a.vy--;
            }
            else if (a.y < b.y)
            {
                b.vy--;
                a.vy++;
            }
            if (a.z > b.z)
            {
                b.vz++;
                a.vz--;
            }
            else if (a.z < b.z)
            {
                b.vz--;
                a.vz++;
            }
        }
        public static void updatePosition(Moon a)
        {
            a.x += a.vx;
            a.y += a.vy;
            a.z += a.vz;
        }
    }
    public class Moon
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public int vx { get; set; }
        public int vy { get; set; }
        public int vz { get; set; }

        public Moon(int x = 0, int y = 0, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            vx = 0;
            vy = 0;
            vz = 0;
        }
        public int getPotentialEnergy()
        {
            int ax = x < 0 ? x * -1 : x;
            int ay = y < 0 ? y * -1 : y;
            int az = z < 0 ? z * -1 : z;
            return ax + ay + az;
        }
        public int getKineticEnergy()
        {
            int avx = vx < 0 ? vx * -1 : vx;
            int avy = vy < 0 ? vy * -1 : vy;
            int avz = vz < 0 ? vz * -1 : vz;
            return avx + avy + avz;
        }
    }
}
