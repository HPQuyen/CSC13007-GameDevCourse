using System.Collections.Generic;

public class SimpleInteraction : IInteraction
{
    private List<InteractionData> listInteractions;
    public SimpleInteraction(List<InteractionData> listInteractions)
    {
        this.listInteractions = listInteractions;
    }
    public IEnumerable<InteractionData> GetInteractions()
    {
        return listInteractions;
    }
}
