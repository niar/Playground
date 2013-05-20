﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecisionTree.Data;
using NUnit.Framework;

namespace DecisionTree.Tests
{
    [TestFixture]
    public class DecisionTreeTests
    {

        const string CAN_SURVIVE_WITHOUT_SURFACING = "can survive without surfacing";
        const string HAS_FLIPPERS = "has flippers";
        const string IS_FISH = "is fish";

        [Test]
        public void TestEntropy()
        {
            var entropy = Decider.Entropy(GetDataSet());

            Assert.That(entropy, Is.EqualTo(0.97095).Within(.00001));
        }

        [Test]
        public void TestSplit()
        {
            var set = GetDataSet();

            var split = set.Split(CAN_SURVIVE_WITHOUT_SURFACING, "1");

            Assert.IsTrue(split.Instances.Count == 3);
        }

        [Test]
        public void TestBestSplit()
        {
            var set = GetDataSet();

            Assert.That(Decider.SelectBestAxis(set), Is.EqualTo(CAN_SURVIVE_WITHOUT_SURFACING));            
        }

        [Test]
        public void CreateTree()
        {
            var tree = GetDataSet().BuildTree();
            tree.DisplayTree();
        }

        [Test]
        public void DecideOnTree()
        {
            var tree = GetDataSet().BuildTree();

            var instance = new Instance
                           {                               
                               Features = new List<Feature>
                                          {
                                              new Feature("1", CAN_SURVIVE_WITHOUT_SURFACING),
                                              new Feature("1", HAS_FLIPPERS)
                                          }
                           };

            var output = Tree.ProcessInstance(tree, instance);

            Assert.That(output.Axis, Is.EqualTo(IS_FISH));
            Assert.That(output.Value, Is.EqualTo("yes"));
        }

        [Test]
        public void DecideOnTreeNoFish()
        {
            var tree = GetDataSet().BuildTree();

            var instance = new Instance
            {
                Features = new List<Feature>
                                          {
                                              new Feature("0", CAN_SURVIVE_WITHOUT_SURFACING),
                                              new Feature("1", HAS_FLIPPERS)
                                          }
            };

            var output = Tree.ProcessInstance(tree, instance);

            Assert.That(output.Axis, Is.EqualTo(IS_FISH));
            Assert.That(output.Value, Is.EqualTo("no"));
        }

        private DataSet GetDataSet()
        {

            #region data

            var instance1 = new Instance
            {
                Output = new Output("yes", IS_FISH),
                Features = new List<Feature>
                                           {
                                               new Feature("1", CAN_SURVIVE_WITHOUT_SURFACING),
                                               new Feature("1", HAS_FLIPPERS)
                                           }
            };

            var instance2 = new Instance
            {
                Output = new Output("yes", IS_FISH),
                Features = new List<Feature>
                                           {
                                               new Feature("1", CAN_SURVIVE_WITHOUT_SURFACING),
                                               new Feature("1", HAS_FLIPPERS)
                                           }
            };

            var instance3 = new Instance
            {
                Output = new Output("no", IS_FISH),
                Features = new List<Feature>
                                           {
                                               new Feature("1", CAN_SURVIVE_WITHOUT_SURFACING),
                                               new Feature("0", HAS_FLIPPERS)
                                           }
            };

            var instance4 = new Instance
            {
                Output = new Output("no", IS_FISH),
                Features = new List<Feature>
                                           {
                                               new Feature("0", CAN_SURVIVE_WITHOUT_SURFACING),
                                               new Feature("1", HAS_FLIPPERS)
                                           }
            };

            var instance5 = new Instance
            {
                Output = new Output("no", IS_FISH),
                Features = new List<Feature>
                                           {
                                               new Feature("0", CAN_SURVIVE_WITHOUT_SURFACING),
                                               new Feature("1", HAS_FLIPPERS)
                                           }
            };

            #endregion

            return new DataSet
            {
                Instances = new List<Instance>
                                          {
                                              instance1,
                                              instance2,
                                              instance3,
                                              instance4,
                                              instance5
                                          }
            };

        }
    }
}
