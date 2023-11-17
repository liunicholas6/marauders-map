type Some<T> = {
    kind : "Some"
    value : T
}

type None = {
    kind: "None"
}

export type Maybe<T> = Some<T> | None

export function Some<T>(v : T) : Maybe<T>{
    return {kind: "Some", value : v}
}

export function None<T>() : Maybe<T> {
    return {kind : "None"}
} 

export function mapMaybe<T, U>(f : (v: T) => U, v : Maybe<T>) : Maybe<U> {
    if (v.kind == "Some") {
        return Some(f(v.value));
    }
    if (v.kind == "None") {
        return None();
    }
}