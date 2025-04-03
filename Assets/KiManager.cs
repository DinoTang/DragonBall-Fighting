// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public class KiManager : DinoBehaviour
// {
//     public List<Slider> kiBars; // Danh sách chứa 3 thanh Ki
//     private float maxKiPerBar = 100f; // Mỗi thanh chứa tối đa 100 Ki
//     private float currentKi = 0f;
//     private float maxKi; // Tổng Ki tối đa
//     protected override void Start()
//     {
//         maxKi = maxKiPerBar * kiBars.Count; // Tổng lượng Ki tối đa
//         ResetKiBars();
//     }

//     void Update()
//     {
//         if (InputManager.Instance.GetChargeInput())
//         {
//             ChargeKi(50 * Time.deltaTime);
//         }
//         if (InputManager.Instance.GetNormalHitInput() || InputManager.Instance.GetKickInput() ||
//          InputManager.Instance.GetStrongHitInput())
//         {
//             UseKi(5000 * Time.deltaTime);
//         }
//     }

//     void ResetKiBars()
//     {
//         foreach (var bar in kiBars)
//         {
//             bar.maxValue = maxKiPerBar;
//             bar.value = 0;
//         }
//     }

//     public void ChargeKi(float amount)
//     {
//         currentKi = Mathf.Clamp(currentKi + amount, 0, maxKi);
//         UpdateKiBars();
//     }

//     public void UseKi(float amount)
//     {
//         currentKi = Mathf.Clamp(currentKi - amount, 0, maxKi);
//         UpdateKiBars();
//     }

//     void UpdateKiBars()
//     {
//         float kiRemaining = currentKi;

//         for (int i = 0; i < kiBars.Count; i++)
//         {
//             if (kiRemaining > maxKiPerBar)
//             {
//                 kiBars[i].value = maxKiPerBar;
//                 kiRemaining -= maxKiPerBar;
//             }
//             else
//             {
//                 kiBars[i].value = kiRemaining;
//                 kiRemaining = 0;
//             }
//         }
//     }
// }
