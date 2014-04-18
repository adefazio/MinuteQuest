
using System;
using System.Collections;
using System.Collections.Generic;

//https://stackoverflow.com/questions/9956486/distributed-probability-random-number-generator

public class LoadedDie {
	// Initializes a new loaded die.  Probs
	// is an array of numbers indicating the relative
	// probability of each choice relative to all the
	// others.  For example, if probs is [3,4,2], then
	// the chances are 3/9, 4/9, and 2/9, since the probabilities
	// add up to 9.
	public LoadedDie(int probs){
		this.prob=new List<long>();
		this.alias=new List<int>();
		this.total=0;
		this.n=probs;
		this.even=true;
	}
	
	Random random=new Random();
	
	List<long> prob;
	List<int> alias;
	long total;
	int n;
	bool even;
	
	public LoadedDie(IEnumerable<int> probs){
		// Raise an error if nil
		if(probs==null)throw new ArgumentNullException("probs");
		this.prob=new List<long>();
		this.alias=new List<int>();
		this.total=0;
		this.even=false;
		var small=new List<int>();
		var large=new List<int>();
		var tmpprobs=new List<long>();
		foreach(var p in probs){
			tmpprobs.Add(p);
		}
		this.n=tmpprobs.Count;
		// Get the max and min choice and calculate total
		long mx=-1, mn=-1;
		foreach(var p in tmpprobs){
			if(p<0)throw new ArgumentException("probs contains a negative probability.");
			mx=(mx<0 || p>mx) ? p : mx;
			mn=(mn<0 || p<mn) ? p : mn;
			this.total+=p;
		}
		// We use a shortcut if all probabilities are equal
		if(mx==mn){
			this.even=true;
			return;
		}
		// Clone the probabilities and scale them by
		// the number of probabilities
		for(var i=0;i<tmpprobs.Count;i++){
			tmpprobs[i]*=this.n;
			this.alias.Add(0);
			this.prob.Add(0);
		}
		// Use Michael Vose's alias method
		for(var i=0;i<tmpprobs.Count;i++){
			if(tmpprobs[i]<this.total)
				small.Add(i); // Smaller than probability sum
			else
				large.Add(i); // Probability sum or greater
		}
		// Calculate probabilities and aliases
		while(small.Count>0 && large.Count>0){
			var l=small[small.Count-1];small.RemoveAt(small.Count-1);
			var g=large[large.Count-1];large.RemoveAt(large.Count-1);
			this.prob[l]=tmpprobs[l];
			this.alias[l]=g;
			var newprob=(tmpprobs[g]+tmpprobs[l])-this.total;
			tmpprobs[g]=newprob;
			if(newprob<this.total)
				small.Add(g);
			else
				large.Add(g);
		}
		foreach(var g in large)
			this.prob[g]=this.total;
		foreach(var l in small)
			this.prob[l]=this.total;
	}
	
	// Returns the number of choices.
	public int Count {
		get {
			return this.n;
		}
	}
	// Chooses a choice at random, ranging from 0 to the number of choices
	// minus 1.
	public int NextValue(){
		var i=random.Next(this.n);
		return (this.even || random.Next((int)this.total)<this.prob[i]) ? i : this.alias[i];
	}
}
