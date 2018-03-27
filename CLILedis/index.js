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
				if(command) {
					request
					.post('https://fierce-everglades-41339.herokuapp.com/api/Ledis')
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
			}
			});
		})
	.parse(process.argv);
	
