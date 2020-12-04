﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Threading;
using NUnit.Framework;
using osu.Framework.Audio;

namespace osu.Framework.Tests.Audio
{
    [TestFixture]
    public class AudioCollectionManagerTest
    {
        [Test]
        public void TestDisposalWhileItemsAreAddedDoesNotThrowInvalidOperationException()
        {
            var manager = new AudioCollectionManager<AdjustableAudioComponent>();

            // add a huge amount of items to the queue
            for (int i = 0; i < 10000; i++) manager.AddItem(new TestingAdjustableAudioComponent());

            // in a seperate thread start processing the queue
            new Thread(() => manager.Update()).Start();

            // wait a little for beginning of the update to start
            Thread.Sleep(4);

            Assert.DoesNotThrow(() => manager.Dispose());
        }

        private class TestingAdjustableAudioComponent : AdjustableAudioComponent
        {
        }
    }
}
