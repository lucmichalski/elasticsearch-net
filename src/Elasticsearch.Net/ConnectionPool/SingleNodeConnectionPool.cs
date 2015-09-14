﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Providers;

namespace Elasticsearch.Net.ConnectionPool
{
	public class SingleNodeConnectionPool : IConnectionPool
	{
		private readonly Node _node;

		public int MaxRetries => 0;

		public bool SupportsReseeding => false;
		public bool SupportsPinging => false;

		public void Reseed(IEnumerable<Node> nodes) { } //ignored
		
		public bool UsingSsl { get; }

		public bool SniffedOnStartup { get { return true; } set {  } }

		public IReadOnlyCollection<Node> Nodes { get; }

		public DateTime LastUpdate { get; set; }

		public SingleNodeConnectionPool(Uri uri, IDateTimeProvider dateTimeProvider = null)
		{
			this._node = new Node(uri);
			this.UsingSsl = this._node.Uri.Scheme == "https";
			this.Nodes = new List<Node> { this._node };
			this.LastUpdate = (dateTimeProvider ?? new DateTimeProvider()).Now();
		}

		public IEnumerable<Node> CreateView()
		{
			return this.Nodes;
		}


	}
}