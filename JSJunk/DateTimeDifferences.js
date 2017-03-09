var date1 = new Date("09/23/1992");
var kur = "23/09/1992";
let pattern = /(\d+)\/(\d+)\/(\d+)/;
let matched = pattern.exec(kur);
console.log(matched[3]);
var date2 = new Date();

var timeDiff = Math.abs(date2.getTime() - date1.getTime());

var diffDays = Math.floor(Math.ceil(timeDiff / (1000 * 3600 * 24)) / 365);
console.log(diffDays);