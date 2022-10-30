export function QuestCreateItemControl(props) {
    return (
        <div className="quest-item">
            <input required name={`QuestItems[${props.index}].Title`} onChange={(event) => props.onChange(props.index, event.target.value) } value={props.value}/>
            <button className="remove-button" type="button" onClick={() => props.onRemove(props.index)}>Remove</button>
        </div>
    );
}
