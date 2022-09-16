var Logo = 'TXSTBXRD';
var version = 'version: sprint 1';
var build = 'build: 64'

var style = ['padding-right: 20px;',
    'min-width: 130px;',
    'padding-left: 20px;',
    'background: black;',
    'line-height: 39px;',
    'color: white;'
].join('');

console.log('%c%s', style, Logo + ': ' + version + ' ' + build);