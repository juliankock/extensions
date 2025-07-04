// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.AI.Evaluation.NLP.Common;
using Xunit;

namespace Microsoft.Extensions.AI.Evaluation.NLP.Tests;

public class NGramTests
{
    [Fact]
    public void Constructor_ValuesAndLength()
    {
        var ngram = new NGram<int>(1, 2, 3);
        Assert.Equal(new[] { 1, 2, 3 }, ngram.Values);
        Assert.Equal(3, ngram.Length);
    }

    [Fact]
    public void Constructor_ThrowsOnEmpty()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new NGram<int>(Array.Empty<int>()));
    }

    [Fact]
    public void Equals_And_HashCode_WorkCorrectly()
    {
        var a = new NGram<int>(1, 2, 3);
        var b = new NGram<int>(1, 2, 3);
        var c = new NGram<int>(3, 2, 1);
        Assert.True(a.Equals(b));
        Assert.True(a.Equals((object)b));
        Assert.False(a.Equals(c));
        Assert.NotEqual(a.GetHashCode(), c.GetHashCode());
    }

    [Fact]
    public void Enumerator_And_IEnumerable()
    {
        var ngram = new NGram<char>('a', 'b', 'c');
        var list = ngram.ToList();
        Assert.Equal(new[] { 'a', 'b', 'c' }, list);
    }

    [Fact]
    public void ToDebugString_FormatsCorrectly()
    {
        var ngram = new NGram<string>("x", "y");
        Assert.Equal("[x,y]", ngram.ToDebugString());
    }

    [Fact]
    public void NGramBuilder_Create_Works()
    {
        NGram<int> ngram = [1, 2];
        Assert.Equal(new NGram<int>(1, 2), ngram);
    }

    [Fact]
    public void NGramGenerationNoPadding()
    {
        int[] input = [1, 2, 3, 4, 5];

        IEnumerable<NGram<int>> result = input.CreateNGrams(1);
        List<NGram<int>> expected = [[1], [2], [3], [4], [5]];
        Assert.True(result.SequenceEqual(expected));

        result = input.CreateNGrams(2);
        expected = [[1, 2], [2, 3], [3, 4], [4, 5]];
        Assert.True(result.SequenceEqual(expected));

        result = input.CreateNGrams(3);
        expected = [[1, 2, 3], [2, 3, 4], [3, 4, 5]];
        Assert.True(result.SequenceEqual(expected));
    }

}
