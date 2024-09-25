#!/bin/bash

compiler="../src/Aurora/Assets/Effects/Compiler/fxc.exe"

if [ ! -x "$compiler" ]; then
    echo "Expected compiler at: '$compiler'"
    exit 1
fi

effects="../src/Aurora/Assets/Effects"

if [ ! -d "$effects" ]; then
    echo "Expected effects at: '$effects'."
    exit 1
fi

for shader in "$effects"/*.fx; do
    input="$shader"
    output="${input%.*}.fxc"

    "$compiler" /T fx_2_0 "$input" /Fo "$output"
    
    echo "Shader output successfully: $input -> $output"
done
