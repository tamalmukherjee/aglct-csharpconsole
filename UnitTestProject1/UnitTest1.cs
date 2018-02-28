namespace UnitTestProject1
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using aglct_csharpconsole;
    using System.Collections.Generic;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            var input = new List<PetOwners>
            {
                new PetOwners
                {
                    age = 18,
                    gender = "Male",
                    name = "Bob",
                    pets = new List<Pet> {
                        new Pet {
                            name = "Kitty",
                            type = "Cat"
                        },
                        new Pet {
                            name = "Rocky",
                            type = "Dog"
                        }
                    }
                },
                new PetOwners
                {
                    age = 18,
                    gender = "Female",
                    name = "Suzan",
                    pets = new List<Pet> {
                        new Pet {
                            name = "Tom",
                            type = "Cat"
                        }
                    }
                },
                new PetOwners
                {
                    age = 19,
                    gender = "Male",
                    name = "Jon",
                    pets = new List<Pet> {
                        new Pet {
                            name = "Snow",
                            type = "Cat"
                        }
                    }
                }
            };            

            //act
            var actualOutput = Program.GetGroupedData(input);

            //assert
            Assert.AreEqual(2, actualOutput.Count);
            Assert.AreEqual("Male", actualOutput[0].OwnerGender);
            Assert.AreEqual(2, actualOutput[0].Cats.Count);
            Assert.AreEqual("Kitty", actualOutput[0].Cats[0]);
            Assert.AreEqual("Snow", actualOutput[0].Cats[1]);
            Assert.AreEqual("Female", actualOutput[1].OwnerGender);
            Assert.AreEqual(1, actualOutput[1].Cats.Count);
            Assert.AreEqual("Tom", actualOutput[1].Cats[0]);
        }
    }
}
