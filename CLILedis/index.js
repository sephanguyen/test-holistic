#!/usr/bin/env node
var request = require('superagent');
var co = require('co');
var prompt = require('co-prompt');
var program = require('commander');

program.arguments('<command>')
	.action(function(start) {
		console.log("Connected");
		co(function *() {
			while(true) {
				var command = yield prompt('');
				request
				.post('http://localhost:60994/api/Ledis')
				.query({query: command})
				.end(function (err, res) {
					if (!err && res.ok) {
						var result = res.text || res.body;
						console.log(result);
					}else {
						console.error(err);
					}
					
				});
			}
			});
		})
	.parse(process.argv);
	