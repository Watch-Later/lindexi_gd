##��¼����
�ύpost����
  url=url/tgrfswo/user/login
�ύ���� 
 - �û�����username
 - ���룺password
����json��ʽ��
`{errorMsg: ,token: }`
 - �û���������ȷʱerrorMsgΪ�գ�tokenΪһ��8λ��������ַ�����ֻ����һ�Σ�
 - �û���������ʱerrorMsg:"�û���������",tokenΪ��
 - �û������ڵ��������errorMsg��"�û������������"��tokenΪ��

 ##ע�������
 �ύpost����url��url/tgrfswo/user/register
 �ύ���ݣ�
  - �û�����username
  - ���룺password
����json��ʽ
`{errorMsg,token}`
  - �û����Ѿ�����ʱerrorMsg:"�û����Ѵ���"��tokenΪ��
  - �û�����ʽ����ȷʱ��errorMsg:"�û�����ʽ����ȷ",tokenΪ��
  - �����ʽ����ȷʱ��errorMsg:"�����ʽ����ȷ",tokenΪ��
  - ע��ɹ�ʱ��errorMsgΪ�գ�tokenΪһ��8λ����������ַ�����ֻ����һ�Σ�

##ע��ɹ�����д��Ҫ������ҵ�Ŀγ̣�
�ύ��post����url��url/tgrfswo/homeWork/setCourse
�ύ���ݣ�
 - token
 - publishCourseName
 ������ҵ�����ƣ�����γ�ʱʹ��Ӣ�Ķ��Ÿ���������һ���ַ����ύ
����json��ʽ��{errorMsg: } 
 - ��token����ʱ��errorMsg:"�����µ�¼"
 - ��token��ȷʱ��errorMsgΪ��

##�û�������ҵ
����post����url:url/tgrfswo/homeWork/getCourse
�ύ���ݣ�token
����json��ʽ��`{errorMsg:,course:[]}`
 - ��token����ʱerrorMsg:"�����µ�¼"��courseΪ��
 - ��token��ȷʱerrorMsgΪ��,�������ݿ���course������

�ύpost����url��url/tgrfswo/homeWork/publish
�ύ���ݣ�
 - token
 - content
 ��ҵ����
 - course
 �γ�
 - submitTime��ҵ�ύʱ��
 ��ʽ��yyyy-MM-dd hh:mm:ss ��ҵ����ʱ��ϵͳĬ�ϣ������ύ
����json��ʽ��{errorMsg:}
 - ��token����errorMsg:"�����µ�¼"
 - ��token��ȷ�����γ�Ϊ�ջ��ڸ��û��Ŀγ��в����ڣ�errorMsg:"�γ����ƴ���"
 - ��token��ȷ�����ύʱ��Ϊ�ջ��ǵ�ǰʱ��Ĺ�ȥʱ,errorMsg:"��ѡ����ȷʱ��"
 - ��token��ȷ����contentΪ�գ�errorMsg:"��ҵ���ݲ���Ϊ��"
 - (token��ȷ��������Ϣ����ʱ������Ϣ��׷����һ���ö��ŷֿ�)
 - ��token��ȷ������������ȷ��errorMsgΪ��

##�û���ע�γ���ҵ����
 �ύpost����url��url/tgrfswo/user/focus
 �ύ���ݣ�token
 ����json��ʽ��{errorMsg:,focusedUsername:[]}
 focusedUsername��ʾ����ע���ˣ������ݿ��е��û�
 - ��token����ʱerrorMsg:"�����µ�¼"��focusedUsernameΪ��
 - ��token��ȷʱerrorMsgΪ��,�������ݿ���focusedUsername������

���û�ѡ��һ������ע��ʱ
�ύurl��url/tgrfswo/user/getCourse
�ύ���ݣ�
 - token
 - focusedUsername
 ����ע������
����json��ʽ��{errorMsg:,course[]}
 - ��token����ʱ��errorMsg:"�����µ�¼"courseΪ��
 - ��token��ȷʱ��errorMsgΪ�գ�course�Ǳ���ע�˵Ŀγ̡�

##��ҵ�����ȡ��ҵʱ
�ύurl��url/tgrfswo/homeWork/get
�ύ�����ݣ�token
 - Token��ȷʱ��������(һ��json����)��[{publisherName:,content:,publishTime:,submitTime:},]
 - Token����ʱ����һ����json��ʽ[{}].
