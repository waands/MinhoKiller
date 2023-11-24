// // Define a base da árvore de decisão
// public abstract class DecisionTreeNode
// {
//     public abstract DecisionTreeNode MakeDecision();
// }

// // Define um nó de decisão
// public class DecisionNode : DecisionTreeNode
// {
//     public DecisionTreeNode trueNode;
//     public DecisionTreeNode falseNode;
//     public System.Func<bool> decision;

//     public DecisionNode(System.Func<bool> decision, DecisionTreeNode trueNode, DecisionTreeNode falseNode)
//     {
//         this.decision = decision;
//         this.trueNode = trueNode;
//         this.falseNode = falseNode;
//     }

//     public override DecisionTreeNode MakeDecision()
//     {
//         // Decide qual nó seguir
//         return decision() ? trueNode : falseNode;
//     }
// }

// // Define um nó de ação
// public class ActionNode : DecisionTreeNode
// {
//     public System.Action action;

//     public ActionNode(System.Action action)
//     {
//         this.action = action;
//     }

//     public override DecisionTreeNode MakeDecision()
//     {
//         // Executa a ação
//         action();
//         return null; // Nós de ação não têm próximo nó
//     }
// }

// // Utilizando a árvore de decisão em sua IA
// public class SnakeAI : MonoBehaviour
// {
//     private DecisionTreeNode rootNode;

//     private void Start()
//     {
//         // Construa a árvore de decisão
//         rootNode = new DecisionNode(
//             ShouldChasePlayer, // A função que decide se deve perseguir o jogador
//             new ActionNode(ChasePlayer), // A ação de perseguir o jogador
//             new ActionNode(GoToFood) // A ação de ir em direção à comida
//         );
//     }

//     private void Update()
//     {
//         // Processa a árvore de decisão
//         MakeDecision();
//     }

//     private void MakeDecision()
//     {
//         DecisionTreeNode node = rootNode;
//         while (node != null)
//         {
//             node = node.MakeDecision();
//         }
//     }

//     private bool ShouldChasePlayer()
//     {
//         // Sua lógica para decidir se deve perseguir o jogador
//         return Vector3.Distance(transform.position, player.position) < 10f; // Exemplo
//     }

//     private void ChasePlayer()
//     {
//         // Sua lógica para perseguir o jogador
//     }

//     private void GoToFood()
//     {
//         // Sua lógica para ir até a comida
//     }
// }

