using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseWork;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        //Створення об'єкту гравців
        Players players = new Players();

        //Перебір
        while (true)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Dices dices = new Dices();

                bool skipPointsAddition = false;
                
                while (dices.RerollCount > 0)
                {
                    //показ того, хто зараз ходить
                    if (players[i].WhoPlays == "player")
                    {
                        Console.WriteLine($"Хід гравця {players[i].Name} №{i + 1}");
                        Console.WriteLine("\nНатисніть будь яку клавішу для кидка кубиків");
                        Console.ReadKey();
                    }
                    else Console.WriteLine($"Хід комп'ютера №{i + 1}");

                    //запoвнення масиву зі значеннями кубиків і їх демонстрація
                    dices.SetRandomDices(dices.RerollCount);

                    //tempSum - сума з комбінацій кубиків
                    //rerollCount - к-сть кубіків, які можна перекинути
                    //int tempSum = CheckDices(dices, i + 1, players[i].Name);
                    
                    dices.Sum.Add(CheckDices(dices, i + 1, players[i].Name));

                    Console.WriteLine($"\nВаша ПОТОЧНА загальна к-сть очок рівна: {dices.Sum.Sum() + players[i].Points}");
                    
                    // НОВА перевірка на додатковий кидок невикористаних кубиків
                    if (dices.RerollCount == 0)
                    {
                        Console.WriteLine("Ви не можете перекинути кубики\n" + "Хід завершено");
                        break;
                    }

                    if (dices.RerollCount == 2 && dices.Sum.Last() == 0 && dices[0].Number == dices[1].Number)
                    {
                        dices.RerollCount = 0;
                        Console.WriteLine("Вам випала щаслива пара {0}{0}\n", dices[0].Number +
                                          "Тому ви можете перекинути відразу всі 5 кубиків\n" +
                                          "Або просто зберегти очки які ви вже заробили за свій хід\n" +
                                          $"Ваша к-сть очок за цей хід рівна {dices.Sum.Sum()}\n" +
                                          $"Загальна к-сть очок БУДЕ рівна {dices.Sum.Sum() + players[i].Points}\n\n" +
                                          "Чи хочете ви перекинути BCI кубики?\n" + "1 - Так\n" + "2 - Ні");
                        
                        if (Method.ReturnOneOrTwo() == 2) break;
                        dices.RerollCount = 5; continue;
                    }

                    if (dices.Sum.Count == 2)
                    {
                        Console.WriteLine("При перекиданнi ");
                        if (dices.Sum[1] == 0)
                        {
                            Console.Write("вам нiчого не випало\n" +
                                          "Тому очки за цей раунд згорають");
                            skipPointsAddition = true;
                            break;
                        }
                        
                        if (dices.RerollCount == 0)
                        {
                            Console.Write("вам повезло\n" +
                                          "Комбінації випали на BCIX перекинутих кубиках\n" +
                                          "Тому у вас є вибiр:\n\n" +
                                          $"1 - Зберегти отриманi вами в цьому раундi очки ({players[i].Points + dices.Sum.Sum()})\n" +
                                          "2 - Перекинути BCI п'ять кбикiв");
                            int tempNum;
                    
                            if (players[i].WhoPlays == "player") tempNum = Method.ReturnOneOrTwo();
                            else tempNum = new Random().Next(1, 3);

                            if (tempNum == 2) { dices.RerollCount = 5; continue; }
                            
                            //dices.RerollCount = 0;
                            Console.WriteLine("Хід завершено");
                            break;
                        }
                        
                        Console.Write("комбінації випали лише на ЧАСТИНI перекинутих кубикiв\n" +
                                      "Тому очки за цей раунд НЕ згорають");
                        break;
                    }
                }
                
                //якщо після перекидання нічого не випало на тих кубиках
                if (skipPointsAddition) { }
                
                //перевірка на "паспорт"
                else if (players[i].Passport)
                {
                    //перевірка чи сума очок менше/рівна за 1000
                    players[i].Points += dices.Sum.Sum();
                }
                else
                {
                    Console.WriteLine("\nВам потрібен \"Паспорт\"");
                    if (dices.Sum.Sum() >= 100)
                    {
                        players[i].Passport = true;
                        players[i].Points += dices.Sum.Sum();
                        Console.WriteLine("Але ви його набрали)");
                    }
                    else Console.WriteLine("У вас не достатньо очків для нього\n" +
                                           "Тому всі очки за цей раунд згорають");
                }

                Console.WriteLine($"\nВаша загальна к-сть очок рівна: {players[i].Points}");
                if (players[i].Points == 1000) InstantWin(1000, players[i].Name, i);
                Console.WriteLine("\nЦей хід завершено");
            }
        }
    }

    static int CheckDices(Dices dices, int playerNum, string playerName)
    {
        //масив, де елемент[i] позначає кубик з номером i+1, а значення елементу це к-сть цих кубиків
        //dices.ConcreteDiceCounter;
        //заповнюємо цей масив ConcreteDiceCounter
        for (int i = 0; i < dices.Length; i++) dices.ConcreteDiceCounter[dices[i].Number - 1].Number++;

        //сума очок
        int sum = 0;

        //перебір кожної цифри кубика по черзі на наявність комбінацій
        for (int i = 1, counter = 0; counter < dices.Length && i <= 6; i++)
        {
            if (dices.ConcreteDiceCounter[i - 1].Number == 0) continue;
            switch (i)
            {
                case 1:
                    counter += dices.ConcreteDiceCounter[0].Number;
                    switch (dices.ConcreteDiceCounter[0].Number)
                    {
                        case 1 or 2:
                            sum += 10 * dices.ConcreteDiceCounter[0].Number;
                            break;
                        case 5:
                            InstantWin(5, playerName, playerNum);
                            break;
                        default:
                            sum += CountSum(dices, 0, 100);
                            break;
                    }
                    break;

                case 2 or 3 or 4 or 6:
                    counter += dices.ConcreteDiceCounter[i - 1].Number;
                    sum += CountSum(dices, i - 1, i * 10);
                    break;
                case 5:
                    counter += dices.ConcreteDiceCounter[4].Number;
                    switch (dices.ConcreteDiceCounter[4].Number)
                    {
                        case 1 or 2:
                            sum += 5 * dices.ConcreteDiceCounter[4].Number;
                            break;
                        default:
                            sum += CountSum(dices, 4, 50);
                            break;
                    }
                    break;
            }
        }

        //перевірка на комбінації { 1, 2, 3, 4, 5 } та { 2, 3, 4, 5, 6 }
        for (int j = 0; j < 2; j++)
        {
            bool flag = true;

            for (int i = 0 + j; i < 5 + j; i++)
            {
                if (dices.ConcreteDiceCounter[i].Number != 1)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                sum += 100 * (j + 1);
                for (int i = 0 + j; i < 5 + j; i++) dices.ConcreteDiceCounter[i].IsUsed = false;
            }
        }

        //підрахунок нової кількості кубиків для перекидання
        NewRerollCount(dices);

        Console.WriteLine($"\nК-сть очок за цей раунд рівна: {sum}");
        Console.WriteLine($"\nК-сть кубиків, які можна перекинути рівна: {dices.RerollCount}");

        return sum;
    }

    //Якось обіграти сумісність різних значень кубиків, їх комбінацій та сум значень
    static int CountSum(Dices dices, int index, int k)
    {
        switch (dices.ConcreteDiceCounter[index].Number)
        {
            case 3:
                return k;
            case 4:
                return k * 2;
            case 5:
                return k * 4;
            case 0:
                return 0;
            default:
                dices.ConcreteDiceCounter[index].IsUsed = true;
                return 0;
        }
    }

    static void NewRerollCount(Dices dices)
    {
        dices.RerollCount = 0;
        
        for (int i = 0; i < 6; i++)
        {
            dices.RerollCount += dices.ConcreteDiceCounter[i].IsUsed ? dices.ConcreteDiceCounter[i].Number : 0 ;
        }
    }

    static void InstantWin(int num, string playerName, int playerNum)
    {
        Console.WriteLine($"Гравець {playerName} №{playerNum} переміг, бо ");
        switch (num)
        {
            case 1000:
                Console.Write("в нього рівно 1000 очок");
                break;
            case 5:
                Console.Write("йому випали одиниці на всіх кубиках");
                break;
        }

        Environment.Exit(0);
    }
}