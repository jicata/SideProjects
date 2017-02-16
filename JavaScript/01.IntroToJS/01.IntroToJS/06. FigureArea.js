function CalcAreaOfFigure(dimensions) {
    var w = Number(dimensions[0]);
    var h = Number(dimensions[1]);
    var W = Number(dimensions[2]);
    var H = Number(dimensions[3]);
    var areaOfInner = 0;
    if(H > h){
        areaOfInner = h*W;
        var areaOfLowerCases = w * h;
        var areaOfUpperCases = W*H;
        var totalArea = (areaOfLowerCases + areaOfUpperCases) - areaOfInner;
    }
    else if(H> h && W>w)
    {
        var totalArea = W*H;
    }
    else if(h> H){
        areaOfInner = w * H;
        var areaOfLowerCases = w * h;
        var areaOfUpperCases = W*H;
        var totalArea = (areaOfLowerCases + areaOfUpperCases) - areaOfInner;
    }


    console.log(totalArea);
}
CalcAreaOfFigure(['1', '1', '2', '2']);