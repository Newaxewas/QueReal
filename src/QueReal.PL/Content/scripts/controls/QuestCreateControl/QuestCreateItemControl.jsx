export function QuestCreateItemControl(props) {
    return (
        <div>
            <input required name={`QuestItems[${props.index}].Title`} onChange={(event) => props.onChange(props.index, event.target.value) } value={props.value}/>
            <button type="button" onClick={() => props.onRemove(props.index)}>Remove</button>
        </div>
    );
}
