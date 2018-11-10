using UnityEngine;
using System.Collections;


public interface IController
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	#endregion

	#region FUNCTIONS

	void SetView(View view);

	void SetModel(Model model);

	void Initialize();

	void EnableController();

	void DisableController();

	#endregion

	#region CLASS_ENUMS

	#endregion
}
 