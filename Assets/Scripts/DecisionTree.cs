public static class DecisionTreeClassifier
{
    public static bool Classify(float distComida, float distPlayer, float aggressao)
    {
        if (distComida <= 2.64f)
        {
            if (distPlayer <= 0.87f)
            {
                if (aggressao <= -0.79f)
                {
                    return false; // class: 0
                }
                else
                {
                    return true; // class: 1
                }
            }
            else if (distPlayer > 0.87f)
            {
                if (distPlayer <= 1.49f)
                {
                    return false; // class: 0
                }
                else // distPlayer > 1.49
                {
                    return false; // class: 0
                }
            }
        }
        else // distComida > 2.64
        {
            if (distPlayer <= 2.93f)
            {
                if (distComida <= 3.22f)
                {
                    return true; // class: 1
                }
                else // distComida > 3.22
                {
                    return true; // class: 1
                }
            }
            else if (distPlayer > 2.93f)
            {
                if (distComida <= 4.58f)
                {
                    return false; // class: 0
                }
                else // distComida > 4.58
                {
                    return true; // class: 1
                }
            }
        }

        return false; // Valor padrão se nenhum caminho for correspondido
    }
}