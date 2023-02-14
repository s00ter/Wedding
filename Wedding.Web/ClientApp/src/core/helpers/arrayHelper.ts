export function RangeFrom(min: number, ct: number) {
    const result = []
    for (let x = min; x <= min + ct; x++) {
        result.push(x)
    }

    return result
}