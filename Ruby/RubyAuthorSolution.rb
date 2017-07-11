n = gets.chomp.to_i
ary = []
for i in 0..n-1
	number = gets.chomp.to_i
	ary.push(number)
end
ary.sort! {|x,y| x <=> y}
ary.take(3).each { |e| print e,"\n"}

