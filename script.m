clc
data = [56 48 51 80 86 119 105 75 109 80 85 118 60 72 98 96 80 50 108 48 51 103 111 109 74 49 110 76 84 64 80 48 48 48 48 48 78 70 80 71 81 63 51 72 48 48 118 98 74 60 100 48 78 48 48 75 115 64 48 49];
for n = 1:length(data)
    if data(n) <  48
        if data(n) == 44 %|| data(n) == 10 || data(n) ==42 || data(n) ==33
            continue;
        end
        disp("less than 48")
        disp("Value:")
        disp(data(n))
    elseif data(n) > 119
        disp("Greater Than 119")
        disp("Value:")
        disp(data(n))
    elseif (data(n) > 87 && data(n) < 96)
        disp("less than 48")
        disp("Value:")
        disp(data(n))
    end
end
disp("Check Complete")