export function QuestItemControl(props) {
    function renderItemId() {
        if (props.value.id === undefined) {
            return null;
        }
        else {
            return <input type="hidden" name={`QuestItems[${props.index}].Id`} value={props.value.id} />
        }
    }

    return (
        <div className="quest-item">
            {renderItemId()}
            <input required name={`QuestItems[${props.index}].Title`} onChange={(event) => props.onChange(props.index, event.target.value)} value={props.value.title} />
            <button className="remove-button" type="button" onClick={() => props.onRemove(props.index)}>Remove</button>
        </div>
    );
}
